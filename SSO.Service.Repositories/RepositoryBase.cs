using SSO.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("SSO.Test")]
namespace SSO.Service.Repositories
{
    public abstract class RepositoryBase
    {
        protected IDataContext AppContext { get; set; }

        /// <summary>
        /// Generic function to execute sql query to return list
        /// </summary>
        /// <typeparam name="TResult">The return type</typeparam>
        /// <param name="sql">The query</param>
        /// <param name="parameters">The Sql Parameters</param>
        /// <returns></returns>
        protected virtual List<TResult> GetListResult<TResult>(string sql, Dictionary<string, object> parameters) where TResult : new()
        {
            List<TResult> result = new List<TResult>();

            if (string.IsNullOrEmpty(sql) || parameters == null)
                return null;

            using (var command = AppContext.CreateCommand())
            {
                command.CommandText = sql;
                foreach (var param in ConstructParameters(parameters))
                {
                    command.Parameters.Add(param);
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add(Common.ModelLoader.Load<TResult>(reader));
                }
            }

            return result;
        }

        /// <summary>
        /// Generic function to execute sql query to return a single record
        /// </summary>
        /// <typeparam name="TResult">The return type</typeparam>
        /// <param name="sql">The query</param>
        /// <param name="parameters">The Sql Parameters</param>
        /// <returns></returns>
        protected virtual TResult GetSingleResult<TResult>(string sql, Dictionary<string, object> parameters) where TResult : new()
        {
            TResult result = new TResult();

            using (var command = AppContext.CreateCommand())
            {
                command.CommandText = sql;
                foreach (var param in ConstructParameters(parameters))
                {
                    command.Parameters.Add(param);
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        result = Common.ModelLoader.Load<TResult>(reader);
                }
            }

            return result;
        }

        /// <summary>
        /// Generic function to construct sql inser query with its parameters and execute it, (using reflection)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual int InsertRecord(object source)
        {
            int result = 0;

            var objectProperties = source.GetType().GetProperties().Where(st =>
                Attribute.IsDefined(st, typeof(ColumnAttribute)) &&
                !Attribute.IsDefined(st, typeof(DatabaseGeneratedAttribute)) &&
                st.GetValue(source) != null)
                .ToList();

            var sql = ConstructInsertStatementWithParameters(source, objectProperties, out Dictionary<string, object> parameters);

            using (var command = AppContext.CreateCommand())
            {
                command.CommandText = sql;

                foreach (var param in ConstructParameters(parameters))
                {
                    command.Parameters.Add(param);
                }

                result = command.ExecuteNonQuery();
            }

            return result;
        }

        /// <summary>
        /// Convert dictionary holding the parameters into sql parameters with proper names that match table columns name
        /// </summary>
        /// <param name="source"></param>
        /// <param name="properties"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal string ConstructInsertStatementWithParameters(object source, List<PropertyInfo> properties, out Dictionary<string, object> parameters)
        {
            StringBuilder result = new StringBuilder(), sqlColumns = new StringBuilder(), sqlValues = new StringBuilder();
            parameters = new Dictionary<string, object>();
            for (int i = 0; i < properties.Count - 1; i++)
            {
                sqlColumns.Append(properties[i].Name).Append(", ");
                sqlValues.Append("@").Append(properties[i].Name).Append(", ");
                parameters.Add(properties[i].Name, properties[i].GetValue(source));
            }

            sqlColumns.Append(properties[properties.Count - 1].Name);
            sqlValues.Append("@").Append(properties[properties.Count - 1].Name);
            parameters.Add(properties[properties.Count - 1].Name, properties[properties.Count - 1].GetValue(source));

            result.Append("INSERT INTO ").Append(source.GetType().Name).Append(" (").Append(sqlColumns).Append(") VALUES(").Append(sqlValues).Append(")");

            return result.ToString();
        }

        /// <summary>
        /// map dictionary values into sql parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal List<SqlParameter> ConstructParameters(Dictionary<string, object> parameters)
        {
            List<SqlParameter> result = new List<SqlParameter>();

            if (parameters == null || parameters.Count < 1)
                return result;

            foreach (var item in parameters)
            {
                result.Add(new SqlParameter(item.Key, item.Value));
            }

            return result;
        }
    }
}

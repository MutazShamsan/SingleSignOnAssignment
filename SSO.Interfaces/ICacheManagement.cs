namespace SSO.Interfaces
{
    public interface ICacheManagement
    {
        void SetOnly(string key, object data);
        object GetOnly(string key);
        void Expire(string key);
    }
}

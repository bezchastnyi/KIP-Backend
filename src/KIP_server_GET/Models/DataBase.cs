namespace KIP_POST_APP.Models
{
    public class DataBase
    {
        public string _name { get; set; }
        public string _db_system { get; set; }
        public string _version { get; set; }
        public string _status { get; set; }

        public DataBase(string name, string db_system, string version, string status)
        {
            this._name = name;
            this._db_system = db_system;
            this._version = version;
            this._status = status;
        }
    }
}

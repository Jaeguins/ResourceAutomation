namespace ProcedureParsing.Commands {

    public interface IProcedureParsable {
        void Set(string location, string value);
        string Get(string location);
        void Initialize();
    }

}
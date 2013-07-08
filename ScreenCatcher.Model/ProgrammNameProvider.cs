namespace ScreenCatcher.Model
{
    public class ProgrammNameProvider
    {
        private const string Paint = "mspaint.exe";
        private const string Explorer = "explorer.exe";

        public static string GetEditProgrammName()
        {
            return Paint;
        }

        public static string GetCatalogProgrammName()
        {
            return Explorer;
        }
    }
}
namespace nacks
{
    class FolderCount
    {
        public int aantal { get; set; }
        public string folderName { get; set; }

        public FolderCount(int _aantal, string _folderName)
        {
            this.aantal = _aantal;
            this.folderName = _folderName;
        }
    }
}
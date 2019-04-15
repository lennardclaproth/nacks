using System;

namespace nacks
{
    class Program
    {
        static void Main(string[] args)
        {
            // FtpConnection conn = new FtpConnection();
            // conn.downloadNacks();

            SortNacks sortNacksInports2 = new SortNacks(2);
            SortNacks sortNacksInports2Gateway = new SortNacks(3);
        }
    }
}

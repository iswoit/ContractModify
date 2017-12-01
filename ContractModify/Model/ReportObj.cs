using System;
using System.Collections.Generic;
using System.Text;

namespace ContractModify
{
    public class ReportObj
    {
        public bool _fileNotExist;
        public string _fileName;
        public int _rowsUpdated;


        public ReportObj(string fileName, int rowsUpdated, bool fileNotExist = false)
        {
            _fileName = fileName;
            _rowsUpdated = rowsUpdated;
            _fileNotExist = fileNotExist;
        }

    }
}

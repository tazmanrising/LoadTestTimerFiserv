using System;
using System.Collections.Generic;
using System.Linq;

namespace EldosFileLib
{
    public class SolFSStreamModeConfig
    {
        public SolFSStreamMode Mode { get; private set; }
        public bool CreateNew { get; private set; }
        public bool ReadEnabled { get; private set; }
        public bool WriteEnabled { get; private set; }

        /// <summary>
        /// Initializes a new instance of the File class.
        /// </summary>
        public SolFSStreamModeConfig(SolFSStreamMode mode)
        {
            Mode = mode;
            SetMode(mode);
        }

        public void SetMode(SolFSStreamMode mode)
        {
            switch (mode)
            {
                case SolFSStreamMode.Write:
                    CreateNew = true;
                    ReadEnabled = false;
                    WriteEnabled = true;
                    break;
                case SolFSStreamMode.Read:
                    CreateNew = false;
                    ReadEnabled = true;
                    WriteEnabled = false;
                    break;
            }
        }

    }
}

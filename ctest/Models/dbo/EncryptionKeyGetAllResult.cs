﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ctest.Models.dboSchema
{
    public partial class EncryptionKeyGetAllResult
    {
        public long ENCRYPTIONKEYID { get; set; }
        public long CHATUSERID { get; set; }
        public byte[] KEYVALUE { get; set; }
        public DateTime CREATEDAT { get; set; }
        public short VALID { get; set; }
        public string MOD_USER { get; set; }
        public DateTime MOD_TIMESTAMP { get; set; }
        public string CR_USER { get; set; }
        public DateTime CR_TIMESTAMP { get; set; }
    }
}

﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ctest.Models.dboSchema
{
    public partial class MessageGetBySenderchatuserIDReceiverchatuserIDResult
    {
        public long MESSAGEID { get; set; }
        public long SENDERCHATUSERID { get; set; }
        public long RECEIVERCHATUSERID { get; set; }
        public string ENCRYPTEDCONTENT { get; set; }
        public DateTime TIMESTAMP { get; set; }
        public short VALID { get; set; }
        public string MOD_USER { get; set; }
        public DateTime MOD_TIMESTAMP { get; set; }
        public string CR_USER { get; set; }
        public DateTime CR_TIMESTAMP { get; set; }
    }
}

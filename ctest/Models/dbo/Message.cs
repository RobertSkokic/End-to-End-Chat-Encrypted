﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ctest.Models.dboSchema;


namespace ctest.Models.dboSchema;

[Table("MESSAGE")]
[Index("Valid", Name = "IX_MESSAGE_VALID", AllDescending = true)]
public partial class Message
{
    [Key]
    [Column("MESSAGEID")]
    public long Messageid { get; set; }

    [Column("SENDERCHATUSERID")]
    public long Senderchatuserid { get; set; }

    [Column("RECEIVERCHATUSERID")]
    public long Receiverchatuserid { get; set; }

    [Required]
    [Column("ENCRYPTEDCONTENT")]
    public string Encryptedcontent { get; set; }

    [Column("TIMESTAMP", TypeName = "datetime")]
    public DateTime Timestamp { get; set; }

    [Column("VALID")]
    public short Valid { get; set; }

    [Required]
    [Column("MOD_USER")]
    [StringLength(256)]
    public string ModUser { get; set; }

    [Column("MOD_TIMESTAMP", TypeName = "datetime")]
    public DateTime ModTimestamp { get; set; }

    [Required]
    [Column("CR_USER")]
    [StringLength(256)]
    public string CrUser { get; set; }

    [Column("CR_TIMESTAMP", TypeName = "datetime")]
    public DateTime CrTimestamp { get; set; }

    [ForeignKey("Receiverchatuserid")]
    [InverseProperty("MessageReceiverchatuser")]
    public virtual Chatuser Receiverchatuser { get; set; }

    [ForeignKey("Senderchatuserid")]
    [InverseProperty("MessageSenderchatuser")]
    public virtual Chatuser Senderchatuser { get; set; }
}
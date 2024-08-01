﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using ctest.Models;
using ctest.Models.dboSchema;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ctest.Models
{
    public partial interface ITest2ContextProcedures
    {
        Task<int> ChatuserDeleteAsync(long? CHATUSERID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ChatuserGetAllResult>> ChatuserGetAllAsync(short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ChatuserGetByChatuserIDResult>> ChatuserGetByChatuserIDAsync(long? CHATUSERID, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ChatuserInsertResult>> ChatuserInsertAsync(string USERNAME, string PASSWORDHASH, DateTime? CREATEDAT, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> ChatuserUpdateAsync(long? CHATUSERID, string USERNAME, string PASSWORDHASH, DateTime? CREATEDAT, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> EncryptionKeyDeleteAsync(long? ENCRYPTIONKEYID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<EncryptionKeyGetAllResult>> EncryptionKeyGetAllAsync(short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<EncryptionKeyGetByChatuserIDResult>> EncryptionKeyGetByChatuserIDAsync(long? CHATUSERID, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<EncryptionKeyGetByEncryptionKeyIDResult>> EncryptionKeyGetByEncryptionKeyIDAsync(long? ENCRYPTIONKEYID, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<EncryptionKeyInsertResult>> EncryptionKeyInsertAsync(long? CHATUSERID, byte[] KEYVALUE, DateTime? CREATEDAT, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> EncryptionKeyUpdateAsync(long? ENCRYPTIONKEYID, long? CHATUSERID, byte[] KEYVALUE, DateTime? CREATEDAT, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> MessageDeleteAsync(long? MESSAGEID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<MessageGetAllResult>> MessageGetAllAsync(short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<MessageGetByMessageIDResult>> MessageGetByMessageIDAsync(long? MESSAGEID, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<MessageGetByReceiverchatuserIDResult>> MessageGetByReceiverchatuserIDAsync(long? RECEIVERCHATUSERID, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<MessageGetBySenderchatuserIDResult>> MessageGetBySenderchatuserIDAsync(long? SENDERCHATUSERID, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<MessageGetBySenderchatuserIDReceiverchatuserIDResult>> MessageGetBySenderchatuserIDReceiverchatuserIDAsync(long? SENDERCHATUSERID, long? RECEIVERCHATUSERID, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<MessageInsertResult>> MessageInsertAsync(long? SENDERCHATUSERID, long? RECEIVERCHATUSERID, string ENCRYPTEDCONTENT, DateTime? TIMESTAMP, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> MessageUpdateAsync(long? MESSAGEID, long? SENDERCHATUSERID, long? RECEIVERCHATUSERID, string ENCRYPTEDCONTENT, DateTime? TIMESTAMP, short? VALID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}

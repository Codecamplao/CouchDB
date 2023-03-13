using CouchDBService.DTO;
using MyCouch.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CouchDBService
{
    public interface ICouchContext
    {
        Task<DocumentHeaderResponse> DeleteAsync(CouchDBHelper couchDBHelper, string id, string rev);
        Task<DocumentHeaderResponse> DeleteFileAsync(CouchDBHelper couchDBHelper, string id, string rev, string filename);
        Task<EntityResponse<T>> EditAsync<T>(CouchDBHelper couchDBHelper, T model) 
            where T : class;
        Task<EntityResponse<T>> EditWithFileAsync<T>(CouchDBHelper couchDBHelper, T model, List<AttachmentRequest> files)
            where T : class;
        Task<string> UploadFileAsync(CouchDBHelper couchDBHelper, string id, string rev, List<AttachmentRequest> files);
        //Task<GetEntityResponse<T>> GetAsync<T>(CouchDBHelper couchDBHelper, string id, string rev = null) 
        //    where T : class;
        //Task<AttachmentResponse> GetFileAsync(CouchDBHelper couchDBHelper, string id, string filename, string rev = null);
        Task<EntityResponse<T>> InsertAsync<T>(CouchDBHelper couchDBHelper, T model) 
            where T : class;
        Task<EntityResponse<T>> InsertWithFileAsync<T>(CouchDBHelper couchDBHelper, T model, List<AttachmentRequest> files) 
            where T : class;
        Task<ViewQueryResponse<T>> ViewQueryAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest keys, int limit, int page, bool reduce, bool desc)
            where T : class 
            where TRequest : class;
        Task<ViewQueryResponse<T>> ViewQueryAsync<T>(CouchDBHelper couchDBHelper, string designName, string viewName, string key, int limit, int page, bool reduce, bool desc) 
            where T : class;
        Task<ViewQueryResponse<T>> ViewQueryAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest[] keys, int limit, int page, bool reduce, bool desc)
            where TRequest : class
            where T : class;
        Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<T>(CouchDBHelper couchDBHelper, string designName, string viewName, string startKey, int limit, int page, bool reduce, bool desc) 
            where T : class;
        Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest startKeys, int limit, int page, bool reduce, bool desc)
            where TRequest : class
            where T : class;
        Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest[] startKeys, int limit, int page, bool reduce, bool desc)
            where TRequest : class
            where T : class;
        Task<ViewQueryResponse<T>> ViewQueryRangeAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest startKeys, TRequest endKeys, int limit, int page, bool reduce, bool desc)
            where TRequest : class
            where T : class;
        Task<(bool CreateStatus, string Reason)> CheckThenCreateDatabase(CouchDBHelper couchDBHelper);
        Task<(bool IsSuccess, string Reason)> BulkDelete(CouchDBHelper couchDBHelper, Dictionary<string, string> bulks);
        Task<DocumentHeaderResponse> DeleteAsync(CouchDBHelper couchDBHelper, string id, string rev, CancellationToken cancellationToken);
        Task<DocumentHeaderResponse> DeleteFileAsync(CouchDBHelper couchDBHelper, string id, string rev, string filename, CancellationToken cancellationToken);
        Task<EntityResponse<T>> EditAsync<T>(CouchDBHelper couchDBHelper, T model, CancellationToken cancellationToken) where T : class;
        Task<EntityResponse<T>> EditWithFileAsync<T>(CouchDBHelper couchDBHelper, T model, List<AttachmentRequest> files, CancellationToken cancellationToken) where T : class;
        Task<string> UploadFileAsync(CouchDBHelper couchDBHelper, string id, string rev, List<AttachmentRequest> files, CancellationToken cancellationToken);
        Task<GetEntityResponse<T>> GetAsync<T>(CouchDBHelper couchDBHelper, string id, string rev = null, CancellationToken cancellationToken = default) where T : class;
        Task<AttachmentResponse> GetFileAsync(CouchDBHelper couchDBHelper, string id, string filename, string rev = null, CancellationToken cancellationToken = default);
        Task<EntityResponse<T>> InsertAsync<T>(CouchDBHelper couchDBHelper, T model, CancellationToken cancellationToken) where T : class;
        Task<EntityResponse<T>> InsertWithFileAsync<T>(CouchDBHelper couchDBHelper, T model, List<AttachmentRequest> files, CancellationToken cancellationToken) where T : class;
        Task<ViewQueryResponse<T>> ViewQueryAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest keys, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken)
            where TRequest : class
            where T : class;
        Task<ViewQueryResponse<T>> ViewQueryAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest[] keys, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken)
            where TRequest : class
            where T : class;
        Task<ViewQueryResponse<T>> ViewQueryAsync<T>(CouchDBHelper couchDBHelper, string designName, string viewName, string key, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken) where T : class;
        Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<T>(CouchDBHelper couchDBHelper, string designName, string viewName, string startKey, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken) where T : class;
        Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest startKeys, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken)
            where TRequest : class
            where T : class;
        Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest[] startKeys, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken)
            where TRequest : class
            where T : class;
        Task<ViewQueryResponse<T>> ViewQueryRangeAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest startKeys, TRequest endKeys, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken)
            where TRequest : class
            where T : class;
        Task<(bool CreateStatus, string Reason)> CheckThenCreateDatabase(CouchDBHelper couchDBHelper, CancellationToken cancellationToken);
        Task<(bool IsSuccess, string Reason)> BulkDelete(CouchDBHelper couchDBHelper, Dictionary<string, string> bulks, CancellationToken cancellationToken);
        Task<(bool CreateStatus, string Reason)> CreateDatabaseView(CouchDBHelper couchDBHelper, string jsonData);
        Task<(bool CreateStatus, string Reason)> CreateDatabaseView(CouchDBHelper couchDBHelper, string jsonData, CancellationToken cancellationToken);
        Task<(bool IsSuccess, string Reason)> DeleteDatabase(CouchDBHelper couchDBHelper);
        Task<(bool IsSuccess, string Reason)> DeleteDatabase(CouchDBHelper couchDBHelper, CancellationToken cancellationToken);
        Task ReplicateDatabase(CouchDBHelper couchDBHelper, ReplicateRequest targetRequest, CancellationToken cancellationToken);
        Task ReplicateDatabase(CouchDBHelper couchDBHelper, ReplicateRequest targetRequest);
        Task<DocumentHeaderResponse> InsertAsync(CouchDBHelper couchDBHelper, string jsonDoc);
        Task<DocumentHeaderResponse> EditAsync(CouchDBHelper couchDBHelper, string id, string rev, string jsonDoc);
        Task<(bool CreateStatus, string Reason)> CreateDatabaseWithView(CouchDBHelper couchDBHelper, string jsonData, CancellationToken cancellationToken);
        Task<(bool CreateStatus, string Reason)> CreateDatabaseWithView(CouchDBHelper couchDBHelper, string jsonData);
        Task QueryAsync(CouchDBHelper couchDBHelper, string selectorExpression, IList<string> fields);
    }
    public class AttachmentRequest
    {
        public string FileName { get; set; }
        public string Ext { get; set; }
        public byte[] Contents { get; set; }

    }
}

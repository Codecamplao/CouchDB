using CouchDBService.DTO;
using MyCouch;
using MyCouch.Requests;
using MyCouch.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace CouchDBService
{
    public class CouchContext : ICouchContext
    {
        public async Task<DocumentHeaderResponse> DeleteAsync(CouchDBHelper couchDBHelper, string id, string rev)
        {
            DocumentHeaderResponse result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                var req = new DeleteDocumentRequest(id, rev);
                result = await client.Documents.DeleteAsync(req);

            }

            return result;
        }
        public async Task<DocumentHeaderResponse> DeleteAsync(CouchDBHelper couchDBHelper, string id, string rev, CancellationToken cancellationToken)
        {
            DocumentHeaderResponse result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                var req = new DeleteDocumentRequest(id, rev);
                result = await client.Documents.DeleteAsync(req, cancellationToken);

            }

            return result;
        }

        public async Task<DocumentHeaderResponse> DeleteFileAsync(CouchDBHelper couchDBHelper, string id, string rev, string filename)
        {
            DocumentHeaderResponse result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                result = await client.Attachments.DeleteAsync(id, rev, filename);

            }

            return result;
        }
        public async Task<DocumentHeaderResponse> DeleteFileAsync(CouchDBHelper couchDBHelper, string id, string rev, string filename, CancellationToken cancellationToken)
        {
            DocumentHeaderResponse result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                result = await client.Attachments.DeleteAsync(id, rev, filename, cancellationToken);

            }

            return result;
        }

        public async Task<EntityResponse<T>> EditAsync<T>(CouchDBHelper couchDBHelper, T model) where T : class
        {
            EntityResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                result = await client.Entities.PutAsync(model);

            }

            return result;
        }
        public async Task<EntityResponse<T>> EditAsync<T>(CouchDBHelper couchDBHelper, T model, CancellationToken cancellationToken) where T : class
        {
            EntityResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                result = await client.Entities.PutAsync(model, cancellationToken);

            }

            return result;
        }

        public async Task<EntityResponse<T>> EditWithFileAsync<T>(CouchDBHelper couchDBHelper, T model, List<AttachmentRequest> files) where T : class
        {
            EntityResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                result = await client.Entities.PutAsync(model);
                string rev = result.Rev;
                // Updload file
                foreach (var f in files)
                {
                    string ctype = MimeMapping.MimeUtility.GetMimeMapping(f.FileName); // get content type
                    var req = new PutAttachmentRequest(result.Id, rev, f.FileName, ctype, f.Contents);
                    var r = await client.Attachments.PutAsync(request: req);

                    rev = r.Rev;
                }
            }

            return result;
        }
        public async Task<EntityResponse<T>> EditWithFileAsync<T>(CouchDBHelper couchDBHelper, T model, List<AttachmentRequest> files, CancellationToken cancellationToken) where T : class
        {
            EntityResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                result = await client.Entities.PutAsync(model);
                string rev = result.Rev;
                // Updload file
                foreach (var f in files)
                {
                    string ctype = MimeMapping.MimeUtility.GetMimeMapping(f.FileName); // get content type
                    var req = new PutAttachmentRequest(result.Id, rev, f.FileName, ctype, f.Contents);
                    var r = await client.Attachments.PutAsync(request: req, cancellationToken);

                    rev = r.Rev;
                }
            }

            return result;
        }

        public async Task<string> UploadFileAsync(CouchDBHelper couchDBHelper, string id, string rev, List<AttachmentRequest> files)
        {
            int i = 0;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                foreach (var f in files)
                {
                    string ctype = MimeMapping.MimeUtility.GetMimeMapping(f.FileName); // get content type
                    var req = new PutAttachmentRequest(id, rev, f.FileName, ctype, f.Contents);
                    var r = await client.Attachments.PutAsync(request: req);

                    rev = r.Rev;
                    i++;
                }

            }
            string result = string.Format($"{0} update file completed", i);
            return await Task.FromResult(result);
        }
        public async Task<string> UploadFileAsync(CouchDBHelper couchDBHelper, string id, string rev, List<AttachmentRequest> files, CancellationToken cancellationToken)
        {
            int i = 0;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                foreach (var f in files)
                {
                    string ctype = MimeMapping.MimeUtility.GetMimeMapping(f.FileName); // get content type
                    var req = new PutAttachmentRequest(id, rev, f.FileName, ctype, f.Contents);
                    var r = await client.Attachments.PutAsync(request: req, cancellationToken);

                    rev = r.Rev;
                    i++;
                }

            }
            string result = string.Format($"{0} update file completed", i);
            return await Task.FromResult(result);
        }

        //public async Task<GetEntityResponse<T>> GetAsync<T>(CouchDBHelper couchDBHelper, string id, string rev = null) where T : class
        //{
        //    GetEntityResponse<T> result;
        //    using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
        //    {
        //        //Using anonymous entities
        //        var req = new GetEntityRequest(id, rev);
        //        result = await client.Entities.GetAsync<T>(req);

        //    }

        //    return result;
        //}
        public async Task<GetEntityResponse<T>> GetAsync<T>(CouchDBHelper couchDBHelper, string id, string rev = null, CancellationToken cancellationToken = default) where T : class
        {
            GetEntityResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                var req = new GetEntityRequest(id, rev);
                result = await client.Entities.GetAsync<T>(req, cancellationToken);
                
            }

            return result;
        }

        //public async Task<AttachmentResponse> GetFileAsync(CouchDBHelper couchDBHelper, string id, string filename, string rev = null)
        //{
        //    AttachmentResponse result;
        //    using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
        //    {
        //        //Using anonymous entities
        //        result = await client.Attachments.GetAsync(id, rev, filename);

        //    }

        //    return result;
        //}
        public async Task<AttachmentResponse> GetFileAsync(CouchDBHelper couchDBHelper, string id, string filename, string rev = null, CancellationToken cancellationToken = default)
        {
            AttachmentResponse result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                result = await client.Attachments.GetAsync(id, rev, filename, cancellationToken);

            }

            return result;
        }

        public async Task<EntityResponse<T>> InsertAsync<T>(CouchDBHelper couchDBHelper, T model) where T : class
        {
            EntityResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {

                //Using anonymous entities
                result = await client.Entities.PostAsync(model);
            }

            return result;
        }
        public async Task<EntityResponse<T>> InsertAsync<T>(CouchDBHelper couchDBHelper, T model, CancellationToken cancellationToken) where T : class
        {
            EntityResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {

                //Using anonymous entities
                result = await client.Entities.PostAsync(model, cancellationToken);
            }

            return result;
        }

        public async Task<EntityResponse<T>> InsertWithFileAsync<T>(CouchDBHelper couchDBHelper, T model, List<AttachmentRequest> files) where T : class
        {
            EntityResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                var res = await client.Entities.PostAsync(model);
                //Gethering Revision
                string rev = res.Rev;

                // try to insert new attachments
                foreach (var f in files)
                {
                    string ctype = MimeMapping.MimeUtility.GetMimeMapping(f.FileName); // get content type
                    var req = new PutAttachmentRequest(res.Id, rev, f.FileName, ctype, f.Contents);
                    var r = await client.Attachments.PutAsync(request: req);
                    rev = r.Rev;
                }

                // get new result
                result = await client.Entities.GetAsync<T>(new GetEntityRequest(res.Id));

            }

            return result;
        }
        public async Task<EntityResponse<T>> InsertWithFileAsync<T>(CouchDBHelper couchDBHelper, T model, List<AttachmentRequest> files, CancellationToken cancellationToken) where T : class
        {
            EntityResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                //Using anonymous entities
                var res = await client.Entities.PostAsync(model, cancellationToken);
                //Gethering Revision
                string rev = res.Rev;

                // try to insert new attachments
                foreach (var f in files)
                {
                    string ctype = MimeMapping.MimeUtility.GetMimeMapping(f.FileName); // get content type
                    var req = new PutAttachmentRequest(res.Id, rev, f.FileName, ctype, f.Contents);
                    var r = await client.Attachments.PutAsync(request: req, cancellationToken);
                    rev = r.Rev;
                }

                // get new result
                result = await client.Entities.GetAsync<T>(new GetEntityRequest(res.Id));

            }

            return result;
        }

        public async Task<ViewQueryResponse<T>> ViewQueryAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest keys, int limit, int page, bool reduce, bool desc) where T : class where TRequest : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (keys == null)
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Keys(keys)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Keys(keys)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }

                }


            }

            return result;
        }
        public async Task<ViewQueryResponse<T>> ViewQueryAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest keys, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken) where T : class where TRequest : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (keys == null)
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Keys(keys)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Keys(keys)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }


            }

            return result;
        }

        public async Task<ViewQueryResponse<T>> ViewQueryAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest[] keys, int limit, int page, bool reduce, bool desc) where T : class where TRequest : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (keys == null)
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Keys(keys)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Keys(keys)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }

                }


            }

            return result;
        }
        public async Task<ViewQueryResponse<T>> ViewQueryAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest[] keys, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken) where T : class where TRequest : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (keys == null)
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Keys(keys)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Keys(keys)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }


            }

            return result;
        }

        public async Task<ViewQueryResponse<T>> ViewQueryAsync<T>(CouchDBHelper couchDBHelper, string designName, string viewName, string key, int limit, int page, bool reduce, bool desc) where T : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (key == "none")
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Key(key)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Key(key)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }

                }
            }

            return result;
        }
        public async Task<ViewQueryResponse<T>> ViewQueryAsync<T>(CouchDBHelper couchDBHelper, string designName, string viewName, string key, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken) where T : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (key == "none")
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Key(key)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Key(key)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }
            }

            return result;
        }

        public async Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<T>(CouchDBHelper couchDBHelper, string designName, string viewName, string startKey, int limit, int page, bool reduce, bool desc) where T : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (startKey == "none")
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKey)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKey)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }

                }
            }

            return result;
        }
        public async Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<T>(CouchDBHelper couchDBHelper, string designName, string viewName, string startKey, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken) where T : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (startKey == "none")
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKey)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKey)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }
            }

            return result;
        }

        public async Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest startKeys, int limit, int page, bool reduce, bool desc) where T : class where TRequest : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (startKeys == null)
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }

                }


            }

            return result;
        }
        public async Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest startKeys, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken) where T : class where TRequest : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (startKeys == null)
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }


            }

            return result;
        }

        public async Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest[] startKeys, int limit, int page, bool reduce, bool desc) where T : class where TRequest : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (startKeys == null)
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }

                }


            }

            return result;
        }
        public async Task<ViewQueryResponse<T>> ViewQueryStartWithAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest[] startKeys, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken) where T : class where TRequest : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (startKeys == null)
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }


            }

            return result;
        }

        public async Task<ViewQueryResponse<T>> ViewQueryRangeAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest startKeys, TRequest endKeys, int limit, int page, bool reduce, bool desc) where T : class where TRequest : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (startKeys == null)
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .EndKey(endKeys)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .EndKey(endKeys)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q);
                    }

                }


            }

            return result;
        }
        public async Task<ViewQueryResponse<T>> ViewQueryRangeAsync<TRequest, T>(CouchDBHelper couchDBHelper, string designName, string viewName, TRequest startKeys, TRequest endKeys, int limit, int page, bool reduce, bool desc, CancellationToken cancellationToken) where T : class where TRequest : class
        {
            ViewQueryResponse<T> result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                if (startKeys == null)
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));


                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }
                else
                {
                    if (limit == -1)
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .EndKey(endKeys)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }
                    else
                    {
                        var q = new QueryViewRequest(designName, viewName).Configure(qry => qry
                        .StartKey(startKeys)
                        .EndKey(endKeys)
                        .Limit(limit).Skip(page)
                        .Reduce(reduce)
                        .Group(reduce)
                        .Descending(desc));

                        result = await client.Views.QueryAsync<T>(q, cancellationToken);
                    }

                }


            }

            return result;
        }

        public async Task<(bool CreateStatus, string Reason)> CheckThenCreateDatabase(CouchDBHelper couchDBHelper)
        {
            using (var client = new MyCouchServerClient(couchDBHelper.ServerAddr))
            {
                var isExist = await client.Databases.GetAsync(couchDBHelper.DbName);
                if (!isExist.IsSuccess)
                {
                    var result = await client.Databases.PutAsync(couchDBHelper.DbName);
                    return (result.IsSuccess, result.Reason);
                }
                return (false, "Database already exist");
                
            }
        }
        public async Task<(bool CreateStatus, string Reason)> CheckThenCreateDatabase(CouchDBHelper couchDBHelper, CancellationToken cancellationToken)
        {
            using (var client = new MyCouchServerClient(couchDBHelper.ServerAddr))
            {
                var isExist = await client.Databases.GetAsync(couchDBHelper.DbName, cancellationToken);
                if (!isExist.IsSuccess)
                {
                    var result = await client.Databases.PutAsync(couchDBHelper.DbName, cancellationToken);
                    return (result.IsSuccess, result.Reason);
                }
                return (false, "Database already exist");

            }
        }

        public async Task<(bool CreateStatus, string Reason)> CreateDatabaseView(CouchDBHelper couchDBHelper, string jsonData)
        {
            // Create View
            using (var cli = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                var viewResult = await cli.Documents.PostAsync(jsonData);

                return (viewResult.IsSuccess, viewResult.Reason);
            }
        }
        public async Task<(bool CreateStatus, string Reason)> CreateDatabaseView(CouchDBHelper couchDBHelper, string jsonData, CancellationToken cancellationToken)
        {
            // Create View
            using (var cli = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                var viewResult = await cli.Documents.PostAsync(jsonData, cancellationToken);

                return (viewResult.IsSuccess, viewResult.Reason);
            }
        }

        public async Task<(bool CreateStatus, string Reason)> CreateDatabaseWithView(CouchDBHelper couchDBHelper, string jsonData)
        {
            using (var client = new MyCouchServerClient(couchDBHelper.ServerAddr))
            {
                var isExist = await client.Databases.GetAsync(couchDBHelper.DbName);
                if (!isExist.IsSuccess)
                {
                    var result = await client.Databases.PutAsync(couchDBHelper.DbName);
                    if (result.IsSuccess)
                    {
                        using (var cli = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
                        {
                            var viewResult = await cli.Documents.PostAsync(jsonData);

                            return (viewResult.IsSuccess, viewResult.Reason);
                        }
                    }
                    return (false, $"Could not create database: {result.Reason}");
                }
                return (false, "Database already exist");

            }
        }
        public async Task<(bool CreateStatus, string Reason)> CreateDatabaseWithView(CouchDBHelper couchDBHelper, string jsonData, CancellationToken cancellationToken)
        {
            using (var client = new MyCouchServerClient(couchDBHelper.ServerAddr))
            {
                var isExist = await client.Databases.GetAsync(couchDBHelper.DbName, cancellationToken);
                if (!isExist.IsSuccess)
                {
                    var result = await client.Databases.PutAsync(couchDBHelper.DbName, cancellationToken);
                    if (result.IsSuccess)
                    {
                        using (var cli = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
                        {
                            var viewResult = await cli.Documents.PostAsync(jsonData, cancellationToken);

                            return (viewResult.IsSuccess, viewResult.Reason);
                        }
                    }
                    return (false, $"Could not create database: {result.Reason}");
                }
                return (false, "Database already exist");

            }
        }

        public async Task<(bool IsSuccess, string Reason)> BulkDelete(CouchDBHelper couchDBHelper, Dictionary<string, string> bulks)
        {
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                var request = new BulkRequest();
                List<DocumentHeader> docs = new List<DocumentHeader>();
                foreach (var item in bulks)
                {
                    docs.Add(new DocumentHeader(item.Key, item.Value));
                }
                request.Delete(docs.ToArray());
                BulkResponse response = await client.Documents.BulkAsync(request);

                return (response.IsSuccess, response.Reason);
            }
        }
        public async Task<(bool IsSuccess, string Reason)> BulkDelete(CouchDBHelper couchDBHelper, Dictionary<string, string> bulks, CancellationToken cancellationToken)
        {
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                var request = new BulkRequest();
                List<DocumentHeader> docs = new List<DocumentHeader>();
                foreach (var item in bulks)
                {
                    docs.Add(new DocumentHeader(item.Key, item.Value));
                }
                request.Delete(docs.ToArray());
                BulkResponse response = await client.Documents.BulkAsync(request, cancellationToken);

                return (response.IsSuccess, response.Reason);
            }
        }

        public async Task<(bool IsSuccess, string Reason)> DeleteDatabase(CouchDBHelper couchDBHelper)
        {
            using (var client = new MyCouchServerClient(couchDBHelper.ServerAddr))
            {
                var result = await client.Databases.DeleteAsync(couchDBHelper.DbName);
                return (result.IsSuccess, result.Reason);
            }
        }
        public async Task<(bool IsSuccess, string Reason)> DeleteDatabase(CouchDBHelper couchDBHelper, CancellationToken cancellationToken)
        {
            using (var client = new MyCouchServerClient(couchDBHelper.ServerAddr))
            {
                var result = await client.Databases.DeleteAsync(couchDBHelper.DbName, cancellationToken);
                return (result.IsSuccess, result.Reason);
            }
        }

        public async Task ReplicateDatabase(CouchDBHelper couchDBHelper, ReplicateRequest targetRequest, CancellationToken cancellationToken)
        {
            using (var client = new MyCouchServerClient(couchDBHelper.ServerAddr))
            {
                string source = $"{couchDBHelper.ServerAddr}/{couchDBHelper.DbName}";
                string target = $"{targetRequest.ServerAddr}/{targetRequest.DbName}";
                var request = new ReplicateDatabaseRequest(Guid.NewGuid().ToString("N"), source, target)
                {
                    Proxy = targetRequest.ServerAddr,
                    Continuous = targetRequest.Continuous,
                    CreateTarget = targetRequest.CreateTarget
                };
                await client.Replicator.ReplicateAsync(request, cancellationToken);
            }
        }
        public async Task ReplicateDatabase(CouchDBHelper couchDBHelper, ReplicateRequest targetRequest)
        {
            using (var client = new MyCouchServerClient(couchDBHelper.ServerAddr))
            {
                string source = $"{couchDBHelper.ServerAddr}/{couchDBHelper.DbName}";
                string target = $"{targetRequest.ServerAddr}/{targetRequest.DbName}";
                var request = new ReplicateDatabaseRequest(Guid.NewGuid().ToString("N"), source, target)
                {
                    Proxy = targetRequest.ServerAddr,
                    Continuous = targetRequest.Continuous,
                    CreateTarget = targetRequest.CreateTarget
                };
                await client.Replicator.ReplicateAsync(request);
            }
        }

        public async Task<DocumentHeaderResponse> InsertAsync(CouchDBHelper couchDBHelper, string jsonDoc)
        {
            DocumentHeaderResponse result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                result = await client.Documents.PostAsync(jsonDoc);
            }

            return result;
        }
        public async Task<DocumentHeaderResponse> EditAsync(CouchDBHelper couchDBHelper, string id, string rev, string jsonDoc)
        {
            DocumentHeaderResponse result;
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                var keyvalues = client.Serializer.Deserialize<IDictionary<string, dynamic>>(jsonDoc);
                keyvalues["_rev"] = rev;
                var doc = client.Serializer.Serialize(keyvalues);
                result = await client.Documents.PutAsync(id, doc);
            }

            return result;
        }
        
        public async Task QueryAsync(CouchDBHelper couchDBHelper, string selectorExpression, IList<string> fields)
        {
            using (var client = new MyCouchClient(couchDBHelper.ServerAddr, couchDBHelper.DbName))
            {
                
                //var q = "{\"name\":{\"$eq\":\"kommaly\"}}";
                var result = await client.Queries.FindAsync(new FindRequest
                { 
                    SelectorExpression = selectorExpression,
                    Fields = fields,//new List<string> { "name", "surname", "address" },
                    //Limit = 1,
                    //Skip = 0,
                    //Sort = null
                });
                
            }
        }
    }
}

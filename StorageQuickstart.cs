﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Cloud.Storage.V1;
using System.Diagnostics;

namespace JustServicios
{
    class StorageQuickstart
    {
        static void Main(string[] args)
        {
            // Your Google Cloud Platform project ID.
            string projectId = "webservicesjustsolution";

            // Instantiates a client.
            StorageClient storageClient = StorageClient.Create();

            // The name for the new bucket.
            string bucketName = projectId + "-test-bucket";
            try
            {
                // Creates the new bucket.
                storageClient.CreateBucket(projectId, bucketName);
                Console.WriteLine($"Bucket {bucketName} created.");
            }
            catch (Google.GoogleApiException e)
            when (e.Error.Code == 409)
            {
                // The bucket already exists.  That's fine.
                Console.WriteLine(e.Error.Message);
            }
        }
    }
}
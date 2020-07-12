using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Extensions;
using Pluralsight.Crypto;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace Server.Security
{
    class CertificateStore
    {
        private const string COMMON_NAME = "Temporalis WindowsRemote SSL CA";
        private const string CERT_FILE_NAME = "TemporalisWindowsRemote.cert";
        private static readonly string CERT_DISTINGUISHED_NAME = $"C=UK, O={AppConstants.ORGANIZATION_NAME}, OU={AppConstants.ORGANIZATION_NAME}, CN={COMMON_NAME}";

        public X509Certificate2 Certificate { get; }

        private CertificateStore(X509Certificate2 certificate)
        {
            this.Certificate = certificate;
        }

        public static bool HasCertificate()
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            return GetCertificateCount(store) >= 1;
        }

        public static CertificateStore LoadCertificate()
        {
            if (HasCertificate())
            {
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadWrite);
                RemoveExpiredAndDuplicateCertificates(store);

                return GetCertificateCount(store) == 0 ?
                    CreateCertificate() :
                    new CertificateStore(GetDomainCertificates(store).ElementAt<X509Certificate2>(0));
            }
            throw new InvalidOperationException("No certificate exists, please create a certificate prior to attempting to load it.");
        }

        public static CertificateStore CreateCertificate()
        {
            using (var ctx = new CryptContext())
            {
                ctx.Open();
                var cert = ctx.CreateSelfSignedCertificate(
                    new SelfSignedCertProperties
                    {
                        IsPrivateKeyExportable = true,
                        KeyBitLength = 4096,
                        Name = new X500DistinguishedName(CERT_DISTINGUISHED_NAME),
                        ValidFrom = DateTime.Today.AddDays(-1),
                        ValidTo = DateTime.Today.AddYears(1),
                    });

                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

                var storePermissions = new StorePermission(PermissionState.Unrestricted);
                storePermissions.Flags = StorePermissionFlags.OpenStore;
                storePermissions.Assert();

                store.Open(OpenFlags.ReadWrite);
                X509Certificate2Collection collection = new X509Certificate2Collection();

                collection.Add(cert);
                store.AddRange(collection);
                store.Close();

                return new CertificateStore(cert);
            }
        }

        private static int GetCertificateCount(X509Store store)
        {
            return GetDomainCertificates(store).Count;
        }

        private static X509Certificate2Collection GetDomainCertificates(X509Store store)
        {
            return store.Certificates.Find(X509FindType.FindBySubjectName, COMMON_NAME, false);
        }

        private static void RemoveExpiredAndDuplicateCertificates(X509Store store)
        {
            var certs = GetDomainCertificates(store);
            var toRemove = new X509Certificate2Collection();
            foreach (var cert in certs)
            {
                if (cert.NotAfter <= DateTime.Now)
                {
                    Console.WriteLine("The certificate has expired, removing it from store");
                    toRemove.Add(cert);
                }
            }
            foreach (var cert in certs)
            {
                if (certs.Count - toRemove.Count == 1)
                {
                    break;
                }
                if (toRemove.Contains(cert))
                {
                    continue;
                }
                Console.WriteLine("The certificate is a duplicate, removing it from store");
                toRemove.Add(cert);
            }
            store.RemoveRange(toRemove);
        }
    }
}

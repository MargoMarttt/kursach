﻿using ClassLibraryForTCPConnectionAPI_C_sharp_;
using DatabaseEntities;
using System;
using System.Collections.Generic;

namespace TCPConnectionAPI_C_sharp_
{
    public class AdminAbilityProtocol : IAdminAbilityProtocol
    {
        protected IMainDBPermission dbContext =
            new DatabaseContext();

        public bool BanClientsWhere(Func<Client, bool> comparer)
        {
            var buf = dbContext.FindClientsWhere(comparer);
            if (buf.Count == 0) return false;
            else
            {
                foreach (var item in buf)
                {
                    item.UserStatus = Status.Banned;
                    dbContext.UpdateClient(item);
                }
                return true;
            }
        }

        public bool BanExpertsWhere(Func<Expert, bool> comparer)
        {
            var buf = dbContext.FindExpertsWhere(comparer);
            if (buf.Count == 0) return false;
            else
            {
                foreach (var item in buf)
                {
                    item.UserStatus = Status.Banned;
                    dbContext.UpdateExpert(item);
                }
                return true;
            }
        }

        public bool DeleteClientsWhere(Func<Client, bool> comparer)
        {
            return dbContext.DeleteClientsWhere(comparer);
        }

        public bool DeleteExpertsWhere(Func<Expert, bool> comparer)
        {
            return dbContext.DeleteExpertsWhere(comparer);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public List<Admin> FindAdminsWhere(Func<Admin, bool> comparer)
        {
            return dbContext.FindAdminsWhere(comparer);
        }

        public List<Client> FindClientsWhere(Func<Client, bool> comparer)
        {
            return dbContext.FindClientsWhere(comparer);
        }

        public List<Expert> FindExpertsWhere(Func<Expert, bool> comparer)
        {
            return dbContext.FindExpertsWhere(comparer);
        }

        public int RegisterNewUser(TypeOfUser type, string login, string password, float rateWeight = 0)
        {
            switch (type)
            {
                case TypeOfUser.Admin:
                    return dbContext.CreateAdmin(new Admin(login, password));
                case TypeOfUser.Client:
                    return dbContext.CreateClient(new Client(login, password));
                case TypeOfUser.Expert:
                    return dbContext.CreateExpert(new Expert(login, password, rateWeight));
                default:
                    return 0;
            }
        }

        public bool UnbanClientsWhere(Func<Client, bool> comparer)
        {
            var buf = dbContext.FindClientsWhere(comparer);
            if (buf == null) return false;
            else
            {
                foreach (var item in buf)
                {
                    if (item.UserStatus == Status.Banned)
                    {
                        item.UserStatus = Status.NotBanned;
                    }
                }
                return true;
            }
        }

        public bool UnbanExpertsWhere(Func<Expert, bool> comparer)
        {
            var buf = dbContext.FindExpertsWhere(comparer);
            if (buf == null) return false;
            else
            {
                foreach (var item in buf)
                {
                    if (item.UserStatus == Status.Banned)
                    {
                        item.UserStatus = Status.NotBanned;
                    }
                }
                return true;
            }
        }

        public string CreateReportAboutVehicles()
        {
            return ReportCreator.CreateReportAboutVehicles();
        }

        public int CreateNewDetailSupplier(DetailSupplier obj)
        {
            return dbContext.CreateDetailSupplier(obj);
        }

        public int CreateNewDetail(Detail obj, string supplierName)
        {
            var supplier = dbContext.FindDetailSuppliers(c => c.Name == supplierName);
            if (supplier.Count != 1) { return 0; }
            supplier[0].Detail.Add(obj);
            dbContext.UpdateDetailSupplier(supplier[0]);
            var buf = dbContext.FindDetailSuppliers(c => c.Name == supplierName);
            var addedObj = buf.Find(c => c.Equals(obj));
            return addedObj.Id;
        }

        public bool UpdateDetailSupplier(DetailSupplier newVersion)
        {
            return dbContext.UpdateDetailSupplier(newVersion);
        }

        public bool UpdateDetail(Detail newVersion)
        {
            return dbContext.UpdateDetail(newVersion);
        }

        public bool DeleteDetailSuppliers(Func<DetailSupplier, bool> func)
        {
            return dbContext.DeleteDetailSupplierWhere(func);
        }

        public bool DeleteDetails(Func<Detail, bool> func)
        {
            return dbContext.DeleteDetailsWhere(func);
        }

        public List<DetailSupplier> FindDetailSuppliers(Func<DetailSupplier, bool> func)
        {
            return dbContext.FindDetailSuppliers(func);
        }

        public List<Detail> FindDetails(Func<Detail, bool> func)
        {
            return dbContext.FindDetails(func);
        }
    }
}

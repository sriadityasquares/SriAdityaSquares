using AutoMapper;
using log4net;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ReconDL
    {
        salesDBEntities dbEntity = new salesDBEntities();
        private static readonly ILog log =
           LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool AddSuppliers(Suppliers s)
        {
            try
            {
                tblSupplier sinfo = new tblSupplier();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Suppliers, tblSupplier>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<Suppliers, tblSupplier>(s, sinfo);
                dbEntity.tblSuppliers.Add(sinfo);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public List<Suppliers> GetSuppliers()
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblSupplier, Suppliers>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<tblSupplier>, List<Suppliers>>(dbEntity.tblSuppliers.ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<Suppliers>();
            }
        }

        public bool UpdateSuppliers(Suppliers s)
        {
            try
            {
                var oldSupplier = dbEntity.tblSuppliers.Where(x => x.SupplierID == s.SupplierID).FirstOrDefault();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Suppliers, tblSupplier>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<Suppliers, tblSupplier>(s, oldSupplier);

                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool AddPaymentVoucher(PaymentVoucher p)
        {
            try
            {
                tblPaymentVoucher pinfo = new tblPaymentVoucher();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PaymentVoucher, tblPaymentVoucher>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<PaymentVoucher, tblPaymentVoucher>(p, pinfo);
                dbEntity.tblPaymentVouchers.Add(pinfo);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public List<PaymentVoucher> GetPaymentVouchers()
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblPaymentVoucher, PaymentVoucher>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<tblPaymentVoucher>, List<PaymentVoucher>>(dbEntity.tblPaymentVouchers.ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<PaymentVoucher>();
            }
        }

        public List<Invoice> GetInvoices()
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblInvoice, Invoice>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<tblInvoice>, List<Invoice>>(dbEntity.tblInvoices.ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<Invoice>();
            }
        }

        public List<InvoiceDetails> GetInvoiceDetails(string QuotationNo)
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblInvoiceDetail, InvoiceDetails>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<tblInvoiceDetail>, List<InvoiceDetails>>(dbEntity.tblInvoiceDetails.Where(x => x.QuotationNo == QuotationNo).OrderBy(x => x.SerialNo).ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<InvoiceDetails>();
            }
        }

        public bool AddInvoice(Invoice inv, List<InvoiceDetails> lstInvoiceDetails)
        {
            try
            {
                tblInvoice invInfo = new tblInvoice();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Invoice, tblInvoice>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<Invoice, tblInvoice>(inv, invInfo);
                dbEntity.tblInvoices.Add(invInfo);


                List<tblInvoiceDetail> dbLstInvDetails = new List<tblInvoiceDetail>();
                foreach (var item in lstInvoiceDetails)
                {
                    tblInvoiceDetail invoiceDetail = new tblInvoiceDetail();
                    var config1 = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<InvoiceDetails, tblInvoiceDetail>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                    });
                    IMapper mapper1 = config1.CreateMapper();
                    mapper1.Map<InvoiceDetails, tblInvoiceDetail>(item, invoiceDetail);
                    //dbLstInvDetails.Add(invoiceDetail);
                    dbEntity.tblInvoiceDetails.Add(invoiceDetail);


                }
                //dbEntity.tblInvoiceDetails.AddRange(dbLstInvDetails);
                dbEntity.SaveChanges();

                return true;

                //List<tblInvoiceDetail> invDetailsInfo = new List<tblInvoiceDetail>();
                //var config1 = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<List<InvoiceDetails>, List<tblInvoiceDetail>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                //});
                //IMapper mapper1 = config1.CreateMapper();
                //mapper1.Map<List<InvoiceDetails>, List<tblInvoiceDetail>>(lstInvoiceDetails, invDetailsInfo);
                //dbEntity.tblInvoiceDetails.AddRange(invDetailsInfo);


            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool UpdateInvoice(Invoice i)
        {
            try
            {
                var lstInvoices = dbEntity.tblInvoices.Where(x => x.QuotationNo == i.QuotationNo).ToList();
                var lstInvoiceDetails = dbEntity.tblInvoiceDetails.Where(x => x.QuotationNo == i.QuotationNo).ToList();
                var lstPaymentVouchers = dbEntity.tblPaymentVouchers.Where(x => x.QuotationNo == i.QuotationNo).ToList();
                if (lstInvoices != null)
                    foreach (var item in lstInvoices)
                        item.InvoiceNo = i.InvoiceNo;
                if (lstInvoiceDetails != null)
                    foreach (var item in lstInvoiceDetails)
                    item.InvoiceNo = i.InvoiceNo;
                if (lstPaymentVouchers != null)
                    foreach (var item in lstPaymentVouchers)
                    item.InvoiceNo = i.InvoiceNo;
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public List<GetReconReport> GetReconReport(string SupplierID)
        {
            List<GetReconReport> lstReconReport = new List<GetReconReport>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetReconReport_Result, GetReconReport>();
                });
                IMapper mapper = config.CreateMapper();
                lstReconReport = mapper.Map<List<sp_GetReconReport_Result>, List<GetReconReport>>(dbEntity.sp_GetReconReport(SupplierID).ToList()).ToList();
                int i = 0;
                string supplierID = "";
                foreach(var item in lstReconReport)
                {
                    if (i == 0)
                    {
                        supplierID = item.SupplierID;
                    }
                    else
                    {
                        if(supplierID == item.SupplierID)
                        {
                            item.InvoiceDate = null;
                            item.InvoiceNo = "";
                            item.CreditAmount = null;
                            supplierID = item.SupplierID;
                        }
                    }
                    i++;

                }
            
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstReconReport;
        }
    }
}

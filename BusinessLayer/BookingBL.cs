﻿using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BookingBL
    {
        BookingDL db = new BookingDL();
        public List<Projects> BindProjects()
        {
            return db.BindProjects();
        }

        public List<Projects> BindAllProjects()
        {
            return db.BindAllProjects();
        }

        public List<Towers> BindTowers(int projectID)
        {
            return db.BindTowers(projectID);
        }

        public List<Flats> BindFlats(int towerID)
        {
            return db.BindFlats(towerID);
        }

        public List<Towers> BindAllTowers(int projectID)
        {
            return db.BindAllTowers(projectID);
        }

        public List<Flats> BindAllFlats(int towerID)
        {
            return db.BindAllFlats(towerID);
        }

        public List<AgentProjectLevel> BindProjectAgents(int projectID)
        {
            return db.BindProjectAgents(projectID);
        }

        public List<FlatDetails> BindFlatDetails(int FlatId, int ProjectID)
        {
            return db.BindFlatDetails(FlatId, ProjectID);
        }

        public bool SaveNewBooking(BookingInformation b)
        {
            return db.SaveNewBooking(b);
        }

        public List<PaymentInformation> BindPaymentDetails(int FlatId)
        {
            return db.BindPaymentDetails(FlatId);
        }
    }
}

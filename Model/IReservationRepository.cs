﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    interface IReservationRepository
    {
       void AddRes(int doctor_id, DateTime res);
    }
}
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OfficeOpenXml.Style;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.Sec;

namespace Application.HubConfig.TimerManager
{
    public class TimerManager
    {
        private int counter = 0;

        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;
        public DateTime TimerStarted { get; }
        public TimerManager(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
            TimerStarted = DateTime.Now;
        }
        public void Execute(object stateInfo)
        {
            _action();
            if ((DateTime.Now - TimerStarted).Seconds > 60)
            {
                _timer.Dispose();
            }

            counter++;
            if (counter == 5)
                _timer.DisposeAsync();
        }
    }
}

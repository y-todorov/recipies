﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace RecipiesModelNS
{
    public partial class Shift : YordanBaseEntity
    {
        public override void Adding(RecipiesModel context, AddEventArgs e)
        {
            Shift shift = this;
            if (shift.StartHour.HasValue)
            {
                shift.StartHour = new DateTime(2000, 1, 1).
                    AddHours(shift.StartHour.Value.Hour).
                    AddMinutes(shift.StartHour.Value.Minute);
            }
            if (shift.EndHour.HasValue)
            {
                shift.EndHour = new DateTime(2000, 1, 1).
                    AddHours(shift.EndHour.Value.Hour).
                    AddMinutes(shift.EndHour.Value.Minute);
            }
            base.Adding(context, e);
        }

        public override void Changing(RecipiesModel context, ChangeEventArgs e)
        {
            Shift shift = this;
            
            if (e.FieldName.Equals("_StartHour", StringComparison.InvariantCultureIgnoreCase))
            {
                DateTime? newStartHour = e.NewValue as DateTime?;
                if (newStartHour.HasValue)
                {
                    shift.StartHour = new DateTime(2000, 1, 1).
                        AddHours(newStartHour.Value.Hour).
                        AddMinutes(newStartHour.Value.Minute);
                }
            }
            if (e.FieldName.Equals("_EndHour", StringComparison.InvariantCultureIgnoreCase))
            {
                DateTime? newEndHour = e.NewValue as DateTime?;
                if (newEndHour.HasValue)
                {
                    shift.EndHour = new DateTime(2000, 1, 1).
                        AddHours(newEndHour.Value.Hour).
                        AddMinutes(newEndHour.Value.Minute);
                }
            }
            base.Changing(context, e);
        }

        public override void Changed(RecipiesModel context, ChangeEventArgs e)
        {           
            base.Changed(context, e);
        }
    }
}

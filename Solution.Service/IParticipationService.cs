﻿using Service.Pattern;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    interface IParticipationService : IService<Participation>
    {
        bool dejaInscrit(int EnfantId, int FormationId);
    }
}
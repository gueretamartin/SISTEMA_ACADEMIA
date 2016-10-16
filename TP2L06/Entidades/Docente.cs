﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Docente : EntidadBase
    {
        private Curso curso;
        private Personas docenteCurso;
        // tipoCargo? private TipoCargo cargo;
        public Curso Curso
        {
            get
            {
                return curso;
            }

            set
            {
                curso = value;
            }
        }

        public Personas DocenteCurso
        {
            get
            {
                return docenteCurso;
            }

            set
            {
                docenteCurso = value;
            }
        }

    }
}


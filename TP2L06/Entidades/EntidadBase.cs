using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EntidadBase
    {
        private States states;
        private int id;

        public EntidadBase()
        {
            this.states = States.New;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        public States State
        {
            get
            {
                return states;
            }

            set
            {
                states = value;
            }
        }
        public enum States
        {
            Deleted,
            New,
            Modified,
            Unmodified
        }
    }
}

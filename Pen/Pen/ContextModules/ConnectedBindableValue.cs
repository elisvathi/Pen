using Pen.UI.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.ContextModules
{
    public class ConnectedBindableValue<T, E> : BindableValue<E>
    {
        //public delegate void ValueChangedForConnector(BindableValue<E>)
        public ConnectedBindableValue(E val) : base(val)
        {
        }
        public void ConnectWith(BindableValue<E> connector)
        {

        }
    }
}

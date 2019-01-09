using System;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Clients
{
    class ClientsAmount : UnitValue
    {
        public ClientsAmount(int value) : base(value, Visitor.Instance)
        { }

        protected ClientsAmount(int value, Unit unit) : base(value, unit)
        { }

        public virtual Service CreateService(NeedType needTypes)
        {
            return new CommonService(needTypes, Value);
        }

        public virtual ClientsAmount Add(ClientsAmount another)
        {
            return new ClientsAmount(Value + another.Value);
        }

        public virtual ClientsAmount Add(int another)
        {
            return new ClientsAmount(Value + another);
        }

        public static ClientsAmount operator +(ClientsAmount left, ClientsAmount right)
        {
            if (left.GetType() != right.GetType())
                throw new Exception("Trying to add deffetent client types");
            return left.Add(right);
        }

        public static ClientsAmount operator +(ClientsAmount left, int right)
        {
            if (left.GetType() != right.GetType())
                throw new Exception("Trying to add deffetent client types");
            return left.Add(right);
        }

        public static ClientsAmount operator +(int left, ClientsAmount right)
        {
            if (left.GetType() != right.GetType())
                throw new Exception("Trying to add deffetent client types");
            return right + left;
        }

        public static ClientsAmount operator *(ClientsAmount left, int right)
        {
            return new ClientsAmount(left.Value * right);
        }

        public static ClientsAmount operator *(int left, ClientsAmount right)
        {
            return right * left;
        }
    }
}

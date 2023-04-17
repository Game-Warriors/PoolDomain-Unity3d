using System;
using System.Reflection;

namespace GameWarriors.PoolDomain.Core
{
    public class BehaviorInitItem
    {
        private static object[][] _argObjects = new object[10][];
        public MethodInfo InitMethod { get; internal set; }
        public ParameterInfo[] InitParamsArray { get; internal set; }
        private PropertyInfo[] Properties { get; set; }

        public BehaviorInitItem(Type type, string methodName)
        {
            Properties = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty);
            InitMethod = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            if (InitMethod != null)
            {
                InitParamsArray = InitMethod.GetParameters();
            }
        }

        internal void SetProperties(IServiceProvider serviceProvider, object instance)
        {
            int length = Properties?.Length ?? 0;
            for (int i = 0; i < length; ++i)
            {
                Type abstractionType = Properties[i].PropertyType;
                if (Properties[i].CanWrite)
                {
                    object service = serviceProvider.GetService(abstractionType);
                    if (service != null)
                        Properties[i].SetValue(instance, service);
                }
            }
        }

        internal void InvokeInit(IServiceProvider serviceProvider, object instance)
        {
            if (InitMethod == null)
                return;

            int length = InitParamsArray?.Length ?? 0;
            if (length > 0)
            {
                object[] args = GetArgumantPool(length);
                for (int i = 0; i < length; ++i)
                {
                    Type argType = InitParamsArray[i].ParameterType;
                    args[i] = serviceProvider.GetService(argType);
                }
                InitMethod.Invoke(instance, args);

                for (int i = 0; i < length; ++i)
                    args[i] = null;
            }
            else
                InitMethod.Invoke(instance, null);
        }

        private object[] GetArgumantPool(int length)
        {
            if (_argObjects.GetLength(0) < length)
                Array.Resize(ref _argObjects, length);

            --length;
            if (_argObjects[length] == null)
                _argObjects[length] = new object[length + 1];

            return _argObjects[length];
        }
    }
}

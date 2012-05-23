using System.Collections.Generic;
using MetaObjectApp;

namespace ETLManager
{
    public class TestMetaObjectFactory : MetaObjectFactory
    {
        public TestMetaObjectFactory(MetaObjectRepository repository)
            :base(repository)
        {
        }
        public override string Name
        {
            get
            {
                return TestMetaObject.Type;
            }
        }
        public override List<AttrNameType> Attributes
        {
            get
            {
                return TestMetaObject.Attributes;
            }
        }

        public override MetaObject CreateObject()
        {
            return new TestMetaObject(_repository);
        }
    }
}

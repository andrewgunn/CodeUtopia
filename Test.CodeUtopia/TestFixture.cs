using System;

namespace Test.CodeUtopia
{
    [Specification]
    public abstract class TestFixture
    {
        protected virtual void Finally()
        {
        }

        protected virtual void Given()
        {
        }

        [Given]
        public void Setup()
        {
            Given();

            try
            {
                When();
            }
            catch (Exception exception)
            {
                Exception = exception;
            }
            finally
            {
                Finally();
            }
        }

        protected abstract void When();

        protected Exception Exception;
    }
}
using Common.Domain.ValueObject;

namespace Common.Domain.Multi
{
    public static class Guard
    {
        public static void UserId(Id id, Id userId)
        {
            Assert.AreEqual(id.ToString(), userId.ToString(), Translation.Key("ERROR.GUARD"));
        }
    }
}

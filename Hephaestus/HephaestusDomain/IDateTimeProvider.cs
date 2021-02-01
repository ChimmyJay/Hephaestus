using System;

namespace HephaestusDomain
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
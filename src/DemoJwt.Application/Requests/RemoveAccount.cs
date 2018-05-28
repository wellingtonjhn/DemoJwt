using System;
using DemoJwt.Application.Core;

namespace DemoJwt.Application.Requests
{
    public class RemoveAccount : Request<Response>
    {
        public Guid Id { get; }

        public RemoveAccount(Guid id)
        {
            Id = id;
        }
    }
}
    namespace G5_MovieTicketBookingSystem.Repositories
    {
        public interface IUserRoleRepository
        {
            public Task<UserRole> AssignRoleToUserAsync(int userId, int roleId);
        }
    }

using BlogApp.API.Models.DTOs.User;
using BlogApp.API.Models.Entities;
using BlogApp.API.Repositories.Interface;
using BlogApp.API.Services.Interface;

namespace BlogApp.API.Services
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Service class for handling user crud operations
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="userRepository">IUserRepository userRepository</param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new user record
        /// </summary>
        /// <param name="requestDto">AddUserDto requestDto</param>
        /// <returns>Returns newly added user data</returns>
        public async Task<AddUserDto> CreateUserAsync(AddUserDto requestDto)
        {
            var user = new User
            {
                CreatedDate = requestDto.CreatedDate,
                EmailAddress = requestDto.EmailAddress,
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Username = requestDto.Username,
                IsDeleted = false,
                HashedPassword = requestDto.HashedPassword
            };

            var userData = await _userRepository.CreateAsync(user);
            if (userData != null)
            {
                return await MapUserData(userData.Id);
            }
            else
            {
                return new AddUserDto();
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        public async Task<int> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Getting all valid users data
        /// </summary>
        /// <returns>Returns lists of users</returns>
        public async Task<IEnumerable<UsersDto>> GetAllAsync()
        {
            var response = new List<UsersDto>();
            var result = await _userRepository.GetAllAsync();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var user = new UsersDto();

                    user.Id = item.Id;
                    user.FirstName = item.FirstName;
                    user.LastName = item.LastName;
                    user.Username = item.Username;
                    user.EmailAddress = item.EmailAddress;
                    user.CreatedDate = item.CreatedDate;

                    if (user != null && user.Id > 0)
                        response.Add(user);
                }
            }
            return response;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing user data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns user data</returns>
        public async Task<AddUserDto> GetUserByIdAsync(int id)
        {
            return await MapUserData(id);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing user record
        /// </summary>
        /// <param name="requestDto">UpdateUserDto requestDto</param>
        /// <returns>Returns updated user data</returns>
        public async Task<UpdateUserDto> UpdateUserAsync(UpdateUserDto requestDto)
        {
            var userData = new UpdateUserDto();
            var user = new User
            {
                Id = requestDto.Id,
                EmailAddress = requestDto.EmailAddress,
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Username = requestDto.Username,
                IsDeleted = false,
                UpdatedDate = requestDto.UpdatedDate,
                HashedPassword = requestDto.HashedPassword
            };

            var result = await _userRepository.UpdateAsync(user);
            if (result != null)
            {
                userData.Id = result.Id;
                userData.Username = result.Username;
                userData.EmailAddress = result.EmailAddress;
                userData.FirstName = result.FirstName;
                userData.LastName = result.LastName;
                userData.HashedPassword = result.HashedPassword;
                userData.UpdatedDate = result.UpdatedDate;
            }
            return userData;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Mapping user data from enity model to DTO model
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns user data</returns>
        private async Task<AddUserDto> MapUserData(int id)
        {
            var userData = new AddUserDto();
            var result = await _userRepository.GetByIdAsync(id);
            if (result != null && result.Id > 0)
            {
                userData.Id = result.Id;
                userData.Username = result.Username;
                userData.EmailAddress = result.EmailAddress;
                userData.FirstName = result.FirstName;
                userData.LastName = result.LastName;
                userData.HashedPassword = result.HashedPassword;
                userData.CreatedDate = result.CreatedDate;
            }
            return userData;
        }
    }
}

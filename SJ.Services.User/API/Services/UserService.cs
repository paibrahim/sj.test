using API.Exceptions;
using API.Models;
using Domain.Aggregate;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserService(UserContext context, CryptoService cryptoService)
    {
        public async Task<GetUserDto> AddUser(AddUserDto dto)
        {
            var userAlreadyExist = await context.Users
                .SingleOrDefaultAsync(x => x.Email.ToLower() == dto.Email.ToLower());

            if (userAlreadyExist != null)
            {
                throw new ArgumentException(nameof(dto.Email));
            }

            var (HashedPassword, Salt) = cryptoService.Hash(dto.Password!);

            var user = User.Create(dto.Name, dto.Email, HashedPassword, Salt, dto.Company);

            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();

            return new GetUserDto
            {
                Id = user.Id!.Value,
                Email = user.Email,
                Name = user.Name,
                Company = user.Company,
            };
        }

        public async Task<List<GetUserDto>> GetAll()
            => await context.Users
            .AsNoTracking()
            .Select(x => new GetUserDto()
            {
                Id = x.Id!.Value,
                Email = x.Email,
                Name = x.Name,
                Company = x.Company,
            })
            .ToListAsync();

        public async Task<GetUserDto> UpdateUser(UpdateUserDto updateUserDto)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(x => x.Id == updateUserDto.Id);

            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            user.Update(updateUserDto.Name, updateUserDto.Email);

            context.Users.Update(user);

            await context.SaveChangesAsync();

            return new GetUserDto()
            {
                Id = user.Id!.Value,
                Email = user.Email,
                Name = user.Name,
                Company = user.Company
            };
        }

        public async Task<GetUserDto> Delete(Guid id)
        {
            var user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            context.Users.Remove(user);

            await context.SaveChangesAsync();

            return new GetUserDto()
            {
                Id = id,
                Email = user.Email,
                Name = user.Name,
                Company = user.Company
            };
        }
    }
}

using System.Net.NetworkInformation;
using Weather.Dtos.User;
using Weather.Model;

namespace Weather.Mappers
{
    public static class UserMappers
    {
        public static Userr ToUserFromCreateUserDTO(this CreateUserRequestDto userDto)
        {
            return new Userr
            { 
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = userDto.Password,
                Age = userDto.Age,
                Gender = userDto.Gender
            };
        }

        public static UserDto ToUserDto (this Userr userModel)
        {
            return new UserDto
            {
                UserId = userModel.UserId,
                UserName = userModel.UserName,
                Email = userModel.Email,
                Password = userModel.Password,
                Age = userModel.Age,
                Gender = userModel.Gender

            };
        }

        //public static Userr ToUserFromGoogleDto(this CreateUserWithGoogle createUseGoogleDto)
        //{
        //    return new Userr
        //    {
        //        UserId = createUseGoogleDto.UserId,
        //        UserName = createUseGoogleDto.UserName,
        //        Email = createUseGoogleDto.Email,
        //        Password = createUseGoogleDto.Password,
        //        Age = createUseGoogleDto.Age,
        //        Gender = createUseGoogleDto.Gender

        //    };
        //}

        public static Userr ToUserFromUpdateDTO(this UpdateUserDto userDto)
        {
            return new Userr
            {
                UserName = userDto.UserName,
                Age = userDto.Age,
                Password = userDto.Password
            };
        }

        

       



    }
}

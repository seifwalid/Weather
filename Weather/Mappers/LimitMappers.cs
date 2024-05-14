using Weather.Dtos.Limit;
using Weather.Model;

namespace Weather.Mappers
{
    public static class LimitMappers
    {
        public static LimitDto ToLimitDto(this PermissibleLimits permissibleLimits)
        {
            return new LimitDto
            {
                Id = permissibleLimits.Id,
                MaxCarbonMonoxide=permissibleLimits.MaxCarbonMonoxide,
                MaxHumidity=permissibleLimits.MaxHumidity,
                MaxNitrogenDioxide = permissibleLimits.MaxNitrogenDioxide,
                MaxOzone = permissibleLimits.MaxOzone,
                MaxPM10 = permissibleLimits.MaxPM10,
                MaxPM2_5 = permissibleLimits.MaxPM2_5,
                MaxSulphurDioxide = permissibleLimits.MaxSulphurDioxide,
                MaxTemperature = permissibleLimits.MaxTemperature
            };
        }

        public static PermissibleLimits ToLimitFromCreateLimitDTO(this CreateLimitDto limitDto)
        {
            return new PermissibleLimits
            {
                MaxCarbonMonoxide=limitDto.MaxCarbonMonoxide,
                MaxHumidity=limitDto.MaxHumidity,
                MaxNitrogenDioxide=limitDto.MaxNitrogenDioxide,
                MaxOzone=limitDto.MaxOzone,
                MaxPM10=limitDto.MaxPM10,
                MaxPM2_5=limitDto.MaxPM2_5,
                MaxSulphurDioxide=limitDto.MaxSulphurDioxide,
                MaxTemperature=limitDto.MaxTemperature
                

            };
        }

        public static PermissibleLimits ToLimitFromUpdateDTO(this UpdateLimitDto limitDto)
        {
            return new PermissibleLimits
            {
                MaxCarbonMonoxide = limitDto.MaxCarbonMonoxide,
                MaxHumidity = limitDto.MaxHumidity,
                MaxNitrogenDioxide = limitDto.MaxNitrogenDioxide,
                MaxOzone = limitDto.MaxOzone,
                MaxPM10 = limitDto.MaxPM10,
                MaxPM2_5 = limitDto.MaxPM2_5,
                MaxSulphurDioxide = limitDto.MaxSulphurDioxide,
                MaxTemperature=limitDto.MaxTemperature
            };
        }
    }
}

using System.Text.Json.Serialization;

namespace SabEnergy.Application.Modules.DTOs.Enerlink;

public class EnerlinkCommandDto
{
    [JsonPropertyName("cmd")]
    public string Cmd { get; set; } = "test_connection";

    [JsonPropertyName("device_id")]
    public string? DeviceId { get; set; }

    [JsonPropertyName("profiles")]
    public List<EnerlinkProfileDto> Profiles { get; set; } = new();

    [JsonPropertyName("channels")]
    public List<EnerlinkChannelDto> Channels { get; set; } = new();
}

public class EnerlinkProfileDto
{
    [JsonPropertyName("profile_id")]
    public string ProfileId { get; set; } = string.Empty;

    [JsonPropertyName("address_offset")]
    public int AddressOffset { get; set; }

    [JsonPropertyName("baud_rate")]
    public int BaudRate { get; set; } = 9600;

    [JsonPropertyName("parity")]
    public string Parity { get; set; } = "E";

    [JsonPropertyName("data_bits")]
    public int DataBits { get; set; } = 8;

    [JsonPropertyName("stop_bit")]
    public int StopBit { get; set; } = 1;

    [JsonPropertyName("parameters")]
    public List<EnerlinkParameterDto> Parameters { get; set; } = new();
}

public class EnerlinkParameterDto
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public int Address { get; set; }

    [JsonPropertyName("data_type")]
    public string DataType { get; set; } = "Float32";

    [JsonPropertyName("byte_order")]
    public string ByteOrder { get; set; } = "CDAB";

    [JsonPropertyName("register_address")]
    public int RegisterAddress { get; set; } = 3;

    [JsonPropertyName("unit")]
    public string Unit { get; set; } = string.Empty;

    [JsonPropertyName("multiplier")]
    public double Multiplier { get; set; } = 1.0;
}

public class EnerlinkChannelDto
{
    [JsonPropertyName("channel_id")]
    public int ChannelId { get; set; }

    [JsonPropertyName("meters")]
    public List<EnerlinkMeterDto> Meters { get; set; } = new();
}

public class EnerlinkMeterDto
{
    [JsonPropertyName("serial_number")]
    public string SerialNumber { get; set; } = string.Empty;

    [JsonPropertyName("slave_id")]
    public int SlaveId { get; set; }

    [JsonPropertyName("profile_id")]
    public string ProfileId { get; set; } = string.Empty;
}

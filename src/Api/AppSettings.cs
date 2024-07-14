namespace Api;

public class AppSettings
{
    public FeatureConfigurations FeatureConfigurations { get; set; }
}
public class FeatureConfigurations
{
    public Authentications Authentications { get; set; }
}
public class Authentications
{
    public string JWTKey { get; set; }
    public int Timeout { get; set; }
}

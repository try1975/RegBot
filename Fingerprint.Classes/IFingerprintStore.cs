namespace Fingerprint.Classes
{
    public interface IFingerprintStore
    {
        Fingerprint GetRandom();
        Fingerprint StoreData(Fingerprint fingerprint);
    }
}

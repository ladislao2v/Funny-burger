namespace Code.UI.Wallet
{
    public interface IWalletView : IView
    {
        public void OnValueChanged(int value);
    }
}
namespace StoreManagementSystem.ViewModels.Wallet;

public record WalletAddViewModel(int StoreId,
    float Income,
    float OutGoing,
    float Balance);
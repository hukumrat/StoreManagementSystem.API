namespace StoreManagementSystem.ViewModels.Wallet;

public record WalletUpdateViewModel(int StoreId,
    float Income,
    float OutGoing,
    float Balance);
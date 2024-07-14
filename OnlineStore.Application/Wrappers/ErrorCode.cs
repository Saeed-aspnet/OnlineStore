namespace OnlineStore.Application.Wrappers;
public enum ErrorCode : short
{
    NotFound = 1,
    InventoryCountIsZero = 2,
    AmountIsInvalid = 3,
    DuplicateData = 4,
    GeneralError = 5
}
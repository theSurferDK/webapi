
// This will contain enums to replace the use of inline/"magic numbers".

namespace BookApp
{
    public enum ErrorCodes_Book : int
    {
        FindingBook = 1,
        SavingBook = 2,
        UpdatingBook = 3,
        DeletingBook = 4
    }

    public enum ErrorCodes_User : int
    {
        FindingUser = 4,
        SavingUser = 5,
        UpdatingUser = 6,
        DeletingUser = 7
    }
}


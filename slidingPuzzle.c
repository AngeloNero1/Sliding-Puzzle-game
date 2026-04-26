#include <stdio.h>
#include <stdlib.h>
#include <time.h>

void printBoard(int board[3][3])
{
    printf("\n");
    for(int i = 0; i < 3; i++)
    {
        for(int j = 0; j < 3; j++)
        {
            if(board[i][j] == 0)
                printf("   ");
            else
                printf("%2d ", board[i][j]);
        }
        printf("\n");
    }
}

int isSolved(int board[3][3])
{
    int count = 1;
    for(int i = 0; i < 3; i++)
    {
        for(int j = 0; j < 3; j++)
        {
            if(i == 2 && j == 2)
                return board[i][j] == 0;

            if(board[i][j] != count)
                return 0;

            count++;
        }
    }
    return 1;
}

void findPosition(int board[3][3], int value, int *row, int *col)
{
    for(int i = 0; i < 3; i++)
    {
        for(int j = 0; j < 3; j++)
        {
            if(board[i][j] == value)
            {
                *row = i;
                *col = j;
                return;
            }
        }
    }
}

int isAdjacent(int r1, int c1, int r2, int c2)
{
    if((abs(r1 - r2) == 1 && c1 == c2) ||
       (abs(c1 - c2) == 1 && r1 == r2))
        return 1;

    return 0;
}

int isSolvable(int arr[9])
{
    int inversions = 0;

    for(int i = 0; i < 9; i++)
    {
        for(int j = i + 1; j < 9; j++)
        {
            if(arr[i] != 0 && arr[j] != 0 && arr[i] > arr[j])
                inversions++;
        }
    }

    return (inversions % 2 == 0);
}

void shuffle(int board[3][3])
{
    int arr[9];

    do {
        for(int i = 0; i < 9; i++)
            arr[i] = i;

        for(int i = 8; i > 0; i--)
        {
            int j = rand() % (i + 1);
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

    } while(!isSolvable(arr));

    int k = 0;
    for(int i = 0; i < 3; i++)
    {
        for(int j = 0; j < 3; j++)
        {
            board[i][j] = arr[k++];
        }
    }
}

int main()
{
    int board[3][3];
    int number;
    int moves = 0;

    srand(time(NULL));
    shuffle(board);

    while(!isSolved(board))
    {
        printBoard(board);
        printf("\nHamle sayisi: %d\n", moves);

        printf("Hangi sayiyi oynatmak istiyorsun (1-8): ");

        if(scanf("%d", &number) != 1)
        {
            printf("Gecersiz giris. Sayi gir.\n");
            while(getchar() != '\n'); // buffer temizle
            continue;
        }

        if(number < 1 || number > 8)
        {
            printf("Gecersiz sayi. 1-8 arasi gir.\n");
            continue;
        }

        int numRow, numCol, zeroRow, zeroCol;

        findPosition(board, number, &numRow, &numCol);
        findPosition(board, 0, &zeroRow, &zeroCol);

        if(isAdjacent(numRow, numCol, zeroRow, zeroCol))
        {
            int temp = board[numRow][numCol];
            board[numRow][numCol] = board[zeroRow][zeroCol];
            board[zeroRow][zeroCol] = temp;

            moves++;
        }
        else
        {
            printf("Hatali hamle. Bu sayi boslukla komsu degil.\n");
        }
    }

    printBoard(board);
    printf("\nTebrikler. Puzzle cozuldu.\n");
    printf("Toplam hamle: %d\n", moves);

    return 0;
} 
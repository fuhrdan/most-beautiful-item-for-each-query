//*****************************************************************************
//** 2070. Most Beautiful Item for Each Query    leetcode                    **
//*****************************************************************************

#include <stdio.h>
#include <stdlib.h>

typedef struct {
    int query;
    int index;
} QueryWithIndex;

int compareItems(const void* a, const void* b) {
    int* itemA = *(int**)a;
    int* itemB = *(int**)b;
    return itemA[0] - itemB[0];
}

int compareQueries(const void* a, const void* b) {
    QueryWithIndex* queryA = (QueryWithIndex*)a;
    QueryWithIndex* queryB = (QueryWithIndex*)b;
    return queryA->query - queryB->query;
}

int* maximumBeauty(int** items, int itemsSize, int* itemsColSize, int* queries, int queriesSize, int* returnSize) {
    *returnSize = queriesSize;
    int* ans = (int*)malloc(queriesSize * sizeof(int));
    QueryWithIndex* queriesWithIndices = (QueryWithIndex*)malloc(queriesSize * sizeof(QueryWithIndex));
    
    for (int i = 0; i < queriesSize; i++) {
        queriesWithIndices[i].query = queries[i];
        queriesWithIndices[i].index = i;
    }
    
    qsort(items, itemsSize, sizeof(int*), compareItems);
    qsort(queriesWithIndices, queriesSize, sizeof(QueryWithIndex), compareQueries);

    int itemIndex = 0;
    int max_beauty = 0;

    int min_query = queriesWithIndices[0].query;
    int max_query = queriesWithIndices[queriesSize - 1].query;

    for (int i = 0; i < queriesSize; i++) {
        int query = queriesWithIndices[i].query;
        int original_index = queriesWithIndices[i].index;

        while (itemIndex < itemsSize && items[itemIndex][0] <= query) {
            if (items[itemIndex][1] > max_beauty) {
                max_beauty = items[itemIndex][1];
            }
            itemIndex++;
        }

        ans[original_index] = max_beauty;
    }

    free(queriesWithIndices);
    return ans;
}

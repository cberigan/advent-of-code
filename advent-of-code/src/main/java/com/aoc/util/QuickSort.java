package com.aoc.util;

public class QuickSort {
	
	public void sort(Integer[] arr){
		if(arr == null || arr.length == 0){
			return;
		}
		this.quickSort(arr,0, arr.length-1);
	}
	
	private void quickSort(Integer[] arr, int start, int end){
		
		if(end <= start) return;
		
		int divider = this.partition(arr, start, end);
		this.quickSort(arr, start, divider-1);
		this.quickSort(arr, divider + 1, end);
		
	}
	
	private void exchange(Integer[] arr, int a, int b){
		int temp = arr[a];
		arr[a] = arr[b];
		arr[b] = temp;
	}

	private int partition(Integer[] arr, int start,int end){
		
		int pivot = arr[end];
		int divider = start;
		
		for(int test = start; test <= end-1;test++){
			if(arr[test] <= pivot){
				this.exchange(arr, divider, test);
				divider++;
			}
		}
		this.exchange(arr, divider, end);
		return divider;
	}
}
package com.aoc.problem3;

import java.io.IOException;
import java.util.Scanner;

import com.aoc.helpers.Resources;
import com.aoc.util.QuickSort;

public class Problem3 {

	public static void main(String[] args) throws IOException {
		
		String[] raw = Resources.LoadTextFile("prob3.txt").split("\n");
		System.out.println("Possible triangles using method 1: " + Method1(raw));
		System.out.println("Possible triangles using method 2: " + Method2(raw));
		
	}
	
	public static Integer Method1(String[] lines){
		
		Integer good = 0;
		
		for(String t : lines){
			Integer[] nums = GetIntArray(t);
			if(IsGood(nums)){
				good++;
			}
		}
		
		return good;
	}
	
	public static Integer Method2(String[] lines){
		Integer good = 0;
		Integer[][] current = new Integer[3][3];
		
		for(int i = 0; i < lines.length;i++){
			
			int row = i % 3;
			Integer[] nums = GetIntArray(lines[i]);
			current[row] = nums; 
			
			if(row == 2){
				
				for(int j = 0; j < 3;j++){
					
					Integer[] triangle = new Integer[3];
					triangle[0] = current[0][j];
					triangle[1] = current[1][j];
					triangle[2] = current[2][j];
					
					if(IsGood(triangle)){
						good++;
					}
				}
			}
		}
		
		return good;
	}
	
	public static boolean IsGood(Integer[] triangle){
		QuickSort qs = new QuickSort();
		qs.sort(triangle);
		return triangle[0] + triangle[1] > triangle[2];
	}

	public static Integer[] GetIntArray(String line){
		Scanner s = new Scanner(line).useDelimiter("\\D+");
		Integer[] ints = new Integer[3];
		ints[0] = s.nextInt();
		ints[1] = s.nextInt();
		ints[2] = s.nextInt();
		s.close();
		return ints;
	}
}

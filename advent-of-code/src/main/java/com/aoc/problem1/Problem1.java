package com.aoc.problem1;

import java.io.IOException;

import com.aoc.helpers.Resources;

public class Problem1 {

	
	
	public static void main(String[] args) throws IOException {
		String[] rawMoves = Resources.LoadTextFile("prob1.txt").split(", ");
		
		TaxiCabEngine t = new TaxiCabEngine();
		
		for(int i = 0; i < rawMoves.length;i++){
			String r = rawMoves[i];
			Turn turn = r.charAt(0)  == 'L' ? Turn.Left : Turn.Right;
			int steps = Integer.parseInt(r.substring(1));
			Move m = new Move(turn,steps);
			t.SendMove(m);
		}
		System.out.println(t.GetDistanceFromHQ());
	
	}

}

package com.aoc.problem4;

import java.util.Comparator;

public class CharComparer implements Comparator<CharCountPair>{

	public int compare(CharCountPair o1, CharCountPair o2) {
		// TODO Auto-generated method stub
		if(o1.getChar() < o2.getChar()){
			return -1;
		}else if(o1.getChar() == o2.getChar()){
			return 0;
		}else{
			return 1;
		}
	}

}

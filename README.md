# RinDotNet

汎用的な高速数値計算ライブラリを目指しています  
使用例
  
```csharp
            //  配列の生成
            float[] vec1 = Enumerable.Range(0, 10000).Select(x => (float)x).ToArray();
            float[] vec2 = Enumerable.Range(0, 10000).Select(x => (float)2 * x).ToArray();

            //Dot積を得る
            float dot = Vector.Dot(vec1, vec2);
            Console.WriteLine(dot);
```
![EZKkzBNUcAEhP9x](https://user-images.githubusercontent.com/16166677/83228956-123df880-a1c2-11ea-83dd-0b86d8443daa.png)

実装済み  
ベクトル内積
  
実装予定  
ベクトル演算  
行列演算  
機械学習  
  
ぜひ支援等よろしくお願いします  
  
@rin_sns_  
https://www.amazon.co.jp/hz/wishlist/ls/IMC1G88FCO7X?ref_=wl_share

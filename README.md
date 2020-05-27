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



実装済み  
ベクトル内積
  
実装予定  
ベクトル演算  
行列演算  
機械学習  
  
ベクトル内積においてはi5 7200uのCPUにて60Gflops超え(要素数10000)を達成しており非常に高速です  
通常の配列を渡すだけで使えるお手軽使用なライブラリを製作していく予定です  
ぜひ支援等よろしくお願いします  
  
@rin_sns_  
https://www.amazon.co.jp/hz/wishlist/ls/IMC1G88FCO7X?ref_=wl_share

export interface IProduct {
  name: string
  description: string
  oldPrice: number
  newPrice: number
  categoryName: string
  images: IImage[]
}

export interface IImage {
  imageName: string
  productId: number
}

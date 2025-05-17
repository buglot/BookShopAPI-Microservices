export interface CouponResponse{
    couponId: number
    couponCode: string
    discountAmount?: number
    minAmount?: number
    lastUpdated?:string|Date
}
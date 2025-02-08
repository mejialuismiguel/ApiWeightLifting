from sqlalchemy import Column, Integer, String, Float
from app.database import Base

class WeightCategory(Base):
    __tablename__ = "weight_category"

    id = Column(Integer, primary_key=True, index=True)
    name = Column(String(50), nullable=False)
    min_weight = Column(Float, nullable=False)
    max_weight = Column(Float, nullable=False)
    gender = Column(String(1), nullable=False)
from sqlalchemy import Column, Integer, String, Date, ForeignKey
from sqlalchemy.orm import relationship
from app.database import Base

class Athlete(Base):
    __tablename__ = "athlete"
    
    id = Column(Integer, primary_key=True, index=True)
    dni = Column(String(20), unique=True, index=True)
    first_name = Column(String(50), index=True)
    last_name = Column(String(50), index=True)
    birth_date = Column(Date)
    gender = Column(String(1), nullable=False)
    country_id = Column(Integer, ForeignKey("country.id"))
    weight_category_id = Column(Integer, ForeignKey("weight_category.id"))

    country = relationship("Country")
    weight_category = relationship("WeightCategory")